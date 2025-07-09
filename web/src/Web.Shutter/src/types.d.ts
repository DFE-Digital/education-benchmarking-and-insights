declare module "gulp-beautify" {
  export function html(options: any): NodeJS.ReadWriteStream;
}

declare module "gulp-nunjucks-render" {
  declare function nunjucksRender(options: any): NodeJS.ReadWriteStream;
  export = nunjucksRender;
}
