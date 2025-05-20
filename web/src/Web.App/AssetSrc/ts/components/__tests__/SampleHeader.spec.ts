import { describe, it, expect } from "vitest";
import { mount } from "@vue/test-utils";
import SampleHeader from "../SampleHeader.vue";

describe("SampleHeader", () => {
  it("renders properly", () => {
    const wrapper = mount(SampleHeader, { props: { msg: "Hello Vitest" } });
    expect(wrapper.text()).toContain("Hello Vitest");
  });
});
