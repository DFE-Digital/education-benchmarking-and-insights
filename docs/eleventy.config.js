import path from 'node:path';
import * as pagefind from 'pagefind';
import { govukEleventyPlugin } from '@x-govuk/govuk-eleventy-plugin';
import { pathPrefix } from './path-prefix.js';

export default function (eleventyConfig) {
  const configuredOutputDir =
    eleventyConfig.directories?.output?.replace(/\/+$/, '') || '_site';

  eleventyConfig.setInputDirectory('content');
  eleventyConfig.setIncludesDirectory('_includes');
  eleventyConfig.addPassthroughCopy('content/assets/images');
  eleventyConfig.addPassthroughCopy('content/assets/js');

  eleventyConfig.addPlugin(govukEleventyPlugin, {
    stylesheets: ['/pagefind/pagefind-ui.css', '/assets/styles.css'],
    titleSuffix: 'FBIT',
    templates: {
      sitemap: true,
      searchIndex: false
    },
    header: {
      logotype: {
        html: '<img src="/assets/images/department-for-education_white.png" alt="Department for Education">'
      },
      search: {
        indexPath: `${pathPrefix}pagefind/pagefind-entry.json`,
        label: 'Search documentation',
        sitemapPath: '/sitemap'
      }
    },
    serviceNavigation: {
      serviceName: 'Financial Benchmarking and Insights Tool',
      navigation: [
        {
          text: 'Tutorials',
          href: '/tutorials/'
        },
        {
          text: 'How-to guides',
          href: '/how-to/'
        },
        {
          text: 'Reference',
          href: '/reference/'
        },
        {
          text: 'Explanation',
          href: '/explanation/'
        }
      ]
    },
    footer: {
      logo: false,
      meta: {
        items: [
          {
            href: 'https://github.com/DFE-Digital/education-benchmarking-and-insights',
            text: 'GitHub repository'
          }
        ],
        html: '<script src="/assets/js/mermaid.js" type="module"></script><script src="/assets/js/pagefind-search.js" type="module"></script>'
      }
    }
  });

  eleventyConfig.on('eleventy.after', async ({ dir, directories }) => {
    const outputDir = directories?.output ?? dir.output;
    const { errors: createErrors, index } = await pagefind.createIndex({
      forceLanguage: 'en',
      excludeSelectors: [
        '.x-govuk-sub-navigation',
        '.govuk-pagination',
        '.govuk-breadcrumbs',
        '.x-govuk-related-navigation',
        '[data-pagefind-ignore]'
      ]
    });

    if (createErrors.length > 0 || !index) {
      throw new Error(`Pagefind index creation failed: ${createErrors.join(', ')}`);
    }

    try {
      const { errors: addErrors } = await index.addDirectory({ path: outputDir });
      if (addErrors.length > 0) {
        throw new Error(`Pagefind directory indexing failed: ${addErrors.join(', ')}`);
      }

      const { errors: writeErrors } = await index.writeFiles({
        outputPath: path.join(outputDir, 'pagefind')
      });
      if (writeErrors.length > 0) {
        throw new Error(`Pagefind bundle write failed: ${writeErrors.join(', ')}`);
      }
    } finally {
      await pagefind.close();
    }
  });

  return {
    pathPrefix: pathPrefix,
    dataTemplateEngine: 'njk',
    htmlTemplateEngine: 'njk',
    markdownTemplateEngine: 'njk',
    dir: {
      input: 'content',
      output: configuredOutputDir
    }
  };
}
