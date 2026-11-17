const excludedLayouts = new Set(['error', 'sitemap']);

export default {
  eleventyComputed: {
    mainAttributes: (data) => {
      if (data.eleventyExcludeFromSearch || excludedLayouts.has(data.layout)) {
        return {
          'data-pagefind-ignore': 'all'
        };
      }

      return {
        'data-pagefind-body': {
          value: true,
          optional: true
        },
        ...(data.sectionKey
          ? { 'data-pagefind-filter': `section:${data.sectionKey}` }
          : {})
      };
    }
  }
};
