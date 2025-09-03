import theoretically from "jest-theories";
import { suggestionFormatter } from "./utils";
import { SuggestResult } from "./types";

describe("Find org utils", () => {
  describe("suggestionFormatter()", () => {
    const theories: { input: SuggestResult<string>; expected: string }[] = [
      {
        input: {
          id: "",
          text: "Future *Acad*emies Watford (Watford, WD25 7HW)",
          document: "",
        },
        expected: "Future <b>Acad</b>emies Watford (Watford, WD25 7HW)",
      },
      {
        input: {
          id: "",
          text: "NCEA Bishop's Primary School (*Acad*emy Road, Ashington, NE63 9FZ)",
          document: "",
        },
        expected:
          "NCEA Bishop's Primary School (<b>Acad</b>emy Road, Ashington, NE63 9FZ)",
      },
      {
        input: {
          id: "",
          text: "Folkestone Primary (*Acad*emy Lane, Folkestone, CT19 5FP)",
          document: "",
        },
        expected:
          "Folkestone Primary (<b>Acad</b>emy Lane, Folkestone, CT19 5FP)",
      },
      {
        input: {
          id: "",
          text: "Knole *Acad*emy (Knole *Acad*emy, Sevenoaks, TN13 3LE)",
          document: "",
        },
        expected:
          "Knole <b>Acad</b>emy (Knole <b>Acad</b>emy, Sevenoaks, TN13 3LE)",
      },
      {
        input: {
          id: "",
          text: "Ryecroft *Acad*emy (Ryecroft *Acad*emy, Leeds, LS12 5AW)",
          document: "",
        },
        expected:
          "Ryecroft <b>Acad</b>emy (Ryecroft <b>Acad</b>emy, Leeds, LS12 5AW)",
      },
    ];

    theoretically(
      "the suggestion {input} is correctly marked up using <b> tags",
      theories,
      (theory) => {
        const output = suggestionFormatter<string>(theory.input);
        expect(output).toBe(theory.expected);
      }
    );
  });
});
