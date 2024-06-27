import { ChartLinkProps } from "./types";

export function ChartLink({ href, children, ...props }: ChartLinkProps) {
  const attributes = {
    xlinkHref: href,
  };
  return (
    <a {...props} {...attributes}>
      {children}
    </a>
  );
}
