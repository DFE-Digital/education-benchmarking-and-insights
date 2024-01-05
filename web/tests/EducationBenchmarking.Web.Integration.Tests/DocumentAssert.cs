using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public static class DocumentAssert
    {
        public static void Breadcrumbs(IHtmlDocument doc, params (string, string)[] breadcrumbs)
        {
            var bcs = doc.GetBreadcrumbs();
            Assert.Collection(bcs,
                breadcrumbs.Select<(string, string), Action<(string, string)>>(expected =>
                {
                    return (actual) => Assert.Equal(expected, actual);
                }).ToArray()
            );
        }

        public static void BackLink(IHtmlDocument doc, (string, string) backLink)
        {
            var (actualText, actualHref) = doc.GetBackLink();
            var (expectedText, expectedHref) = backLink;
            Assert.Equal(expectedText, actualText);
            Assert.Equal(expectedHref, actualHref);
        }

        public static void TitleAndH1(IHtmlDocument doc, string pageTitle, string header1)
        {
            var h1s = doc.QuerySelector("h1");
            
            if (h1s != null)
            {
                var heading = String.Join(" ", h1s.ChildNodes.Select(n => n.TextContent.Trim())).Trim();
                Assert.Equal(pageTitle, doc.Title);
                Assert.Equal(header1, heading);
            }
            else
            {
                throw new Exception("No <h1> elements found in this page document");
            }
        }
        
        public static void Heading2(IHtmlDocument doc, string header2)
        {
            var h2 = doc.QuerySelector("h2");
            var heading2 = String.Join(" ", h2.ChildNodes.Select(n => n.TextContent.Trim())).Trim();
            Assert.Equal(header2, heading2);
        }

        public static void Heading3(IHtmlDocument doc, string header3)
        {
            var h3 = doc.QuerySelector("h3");
            var heading2 = String.Join(" ", h3.ChildNodes.Select(n => n.TextContent.Trim())).Trim();
            Assert.Equal(header3, heading2);
        }

        public static void AssertPageUrl(IHtmlDocument doc, string expectedUrl)
        {
            Assert.Equal(expectedUrl, doc.Url);
        }
        
        public static void AssertPageUrlStartsWith(IHtmlDocument doc, string expectedUrl)
        {
            Assert.StartsWith(expectedUrl, doc.Url);
        }

        public static void ErrorSummary(IHtmlDocument doc, params string[] errors)
        {
            var errorSummaryLinks = doc.QuerySelectorAll(".govuk-error-summary__body a").OfType<IHtmlAnchorElement>().ToList();
            Assert.Equal(errors.Length, errorSummaryLinks.Count);

            foreach (var (expectedText, errorSummaryLink) in errors.Zip(errorSummaryLinks))
            {
                Assert.Equal(expectedText, errorSummaryLink.TextContent.Trim());

                var fragment = new Uri(errorSummaryLink.Href).Fragment.TrimStart('#');
                if (!string.IsNullOrEmpty(fragment))
                {
                    // Check there is an element we can navigate to
                    Assert.NotNull(doc.GetElementById(fragment));
                    // Check there is an explanation attached to the field itself
                    Assert.NotNull(doc.QuerySelector($".field-validation-error[data-valmsg-for='{fragment}']"));
                }
            }
        }

        public static void SuccessBanner(IHtmlDocument doc, string expectedBodyContent)
        {
            var successBanner =
                doc.GetElementsByClassName("govuk-notification-banner govuk-notification-banner--success")
                    .FirstOrDefault();
            Assert.NotNull(successBanner);

            var successBannerTitle = successBanner!.GetElementsByClassName("govuk-notification-banner__title").Single();
            Assert.Contains("Success", successBannerTitle.TextContent);
            
            var successBannerBody = successBanner!.GetElementsByClassName("govuk-notification-banner__content").Single();
            Assert.Contains(expectedBodyContent, successBannerBody.TextContent);
        }

        public static void TextEqual(IElement element, string expected)
        {
            Assert.Equal(expected, element.TextContent.Trim());
        }

        public static void PrimaryCTA(IElement element, string contents, string url, bool enabled = true)
        {
            Assert.Equal(contents, element.TextContent.Trim());

            if (element is IHtmlLinkElement a)
            {
                Assert.Equal(url, a.Href);
            }
            
            if (element is IHtmlAnchorElement c)
            {
                Assert.Equal(url, c.PathName + c.Search);
            }

            if (element is IHtmlButtonElement b)
            {
                Assert.Equal(url, b.Form?.Action);
            }

            Assert.True(element.ClassList.Contains("govuk-button"),
                "The primary CTA should have a the class govuk-button assigned");

            if (!enabled)
            {
                Assert.True(element.ClassList.Contains("govuk-button--disabled"),
                    $"The {element.Id} should have govuk-button--disabled class supplied");
            }
        }

        public static void SecondaryCTA(IElement element, string contents, string url, bool enabled = true)
        {
            Assert.Equal(contents, element.TextContent.Trim());

            if (element is IHtmlLinkElement a)
            {
                Assert.Equal(url, a.Href);
            }

            if (element is IHtmlAnchorElement an)
            {
                Assert.Equal(url, new Uri(an.Href).PathAndQuery);
            }
            
            if (element is IHtmlButtonElement b)
            {
                Assert.Equal(url, b.Form.Action);
            }

            Assert.True(element.ClassList.Contains("govuk-button"),
                "The secondary CTA should have a the class govuk-button assigned");
            Assert.True(element.ClassList.Contains("govuk-button--secondary"),
                "The secondary CTA should have a the class govuk-button--secondary assigned");

            if (!enabled)
            {
                Assert.True(element.ClassList.Contains("govuk-button--disabled"),
                    $"The {element.Id} should have govuk-button--disabled class supplied");
            }
        }
        
        public static void Link(IElement element, string contents, string url)
        {
            Assert.Equal(contents, element.TextContent.Trim());

            if (element is IHtmlAnchorElement a)
            {
                Assert.Equal(url, a.Href);
            }

            if (element is IHtmlLinkElement l)
            {
                Assert.Equal(url, l.Href);
            }

            Assert.True(element.ClassList.Contains("govuk-link"),"A link should have a the class govuk-link");
        }

        public static void Table(IElement element, List<List<string>> data, bool removeHeadingSpan = false,
            bool removeAllWhitespace = false)
        {
            var table = Assert.IsAssignableFrom<IHtmlTableElement>(element);
            var actual = table.Rows.Select(r =>
            {
                var cells = new List<string>();
                foreach (var cell in r.Cells)
                {
                    if (removeHeadingSpan)
                    {
                        var spanElement = cell.GetElementsByClassName("responsive-table__heading").FirstOrDefault();
                        if (spanElement != null)
                            cell.RemoveElement(spanElement);
                    }

                    var textContent = removeAllWhitespace ? cell.TextContent.Replace(" ", "").Replace("\n", "") : cell.TextContent;

                    cells.Add(textContent.Trim());
                }

                return cells;
            }).ToList();

            Assert.Equal(data, actual);
        }

        public static void Select(IElement element, List<string> expectedOptions)
        {
            Assert.True(element is IHtmlSelectElement);

            var selectOptions = element.Children.Where(e => e is IHtmlOptionElement).Select(e => e.TextContent)
                .ToList();
            for (int i = 0; i < expectedOptions.Count; i++)
            {
                Assert.Equal(expectedOptions.ElementAt(i), selectOptions.ElementAt(i));
            }
        }

        public static void IsErrorUrnPage(IHtmlDocument doc)
        {
            TitleAndH1(doc, "Error - No associated URN", "You cannot continue");
            TextEqual(doc.FindMainContentElements<IHtmlParagraphElement>().First(),
                "DfE Sign-in do not have a Unique Reference Number (URN) associated with your school in their system.");
            Link(doc.FindMainContentElements<IHtmlAnchorElement>().First(), "Contact DfE Sign-in",
                "https://help.signin.education.gov.uk/contact/submit?type=service-access&service=Assessment%20Service");
        }

        public static void IsErrorNotFoundPage(IHtmlDocument doc)
        {
            TitleAndH1(doc, "Page not found", "Page not found");

            var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();

            TextEqual(paras[0], "If you typed the web address, check it is correct.");
            TextEqual(paras[1], "If you pasted the web address, check you copied the entire address.");
            TextEqual(paras[2],
                "If the web address is correct or you selected a link or button, go to the contact us page for further support.");

            var contactUsLink = doc.FindMainContentElements<IHtmlAnchorElement>().First();

            Link(contactUsLink, "contact us", "https://localhost/Content/Contact");
        }

        public static void IsInvalidAssessmentPage(IHtmlDocument doc)
        {
            TitleAndH1(doc, "Error - Invalid Assessment Id", "This assessment is not available");
            var genericErrorMessage = doc
                .QuerySelectorAll("p")
                .Any(x => x.TextContent ==
                          "To manage a different assessment, choose an assessment within the Manage your school assessments service.");
            Assert.True(genericErrorMessage, "Cannot find generic error message");
        }

        public static void IsErrorUnauthenticated(IHtmlDocument doc)
        {
            TitleAndH1(doc, "Access denied", "Your account does not have sufficient privileges");

            var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();

            TextEqual(paras[0], "To get access, contact your school's DfE Sign-in approver.");

            var contactUsLink = doc.FindMainContentElements<IHtmlAnchorElement>().First();

            Link(contactUsLink, "Return to home", "https://localhost/");
        }

        public static void IsErrorUnauthorised(IHtmlDocument doc)
        {
            TitleAndH1(doc, "Access denied", "You no longer have permission to access this part of the service");

            var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();
            
            TextEqual(paras[0], "To get access, contact your school's DfE Sign-in approver.");

            var contactUsLink = doc.FindMainContentElements<IHtmlAnchorElement>().First();

            Link(contactUsLink, "Return to home", "https://localhost/");
        }

        public static void IsServerError(IHtmlDocument doc)
        {
            TitleAndH1(doc, "Service unavailable", "Sorry, there is a problem with the service");

            var paras = doc.FindMainContentElements<IHtmlParagraphElement>().ToArray();

            TextEqual(paras[0], "Try again later.");
            TextEqual(paras[1], "Go to the contact us page for further support.");

            var contactUsLink = doc.FindMainContentElements<IHtmlAnchorElement>().First();

            Link(contactUsLink, "contact us", "https://localhost/Content/Contact");
        }

        public static void Radios(IElement parent, params (string, string, string)[] options)
        {
            var index = 0;
            foreach (var radioItem in parent.Descendents<IElement>()
                .Where(c => c.ClassList.Contains("govuk-radios__item")))
            {
                var (name, value, label) = options[index];
                var inputElement = Assert.IsAssignableFrom<IHtmlInputElement>(radioItem.Children[0]);
                var labelElement = Assert.IsAssignableFrom<IHtmlLabelElement>(radioItem.Children[1]);

                ValidateInputElement(name, value, inputElement);
                ValidateLabelElement(label, labelElement);

                Assert.Equal(inputElement.Id, labelElement.HtmlFor);
                index++;
            }

            void ValidateInputElement(string name, string value, IHtmlInputElement e)
            {
                Assert.Equal("radio", e.Type);
                Assert.Equal(name, e.Name);
                Assert.Equal(value, e.Value);
                Assert.True(e.ClassList.Contains("govuk-radios__input"),
                    "A radio input should have the 'govuk-radios__item' class applied");
            }

            void ValidateLabelElement(string label, IHtmlLabelElement e)
            {
                Assert.Equal(label, e.TextContent.Trim());
                Assert.True(e.ClassList.Contains("govuk-label") && e.ClassList.Contains("govuk-radios__label"),
                    "A radio input should have the 'govuk-label' and 'govuk-radios__label' classes applied");
            }
        }

        public static void RadioHint(IHtmlDocument doc, string id)
        {
            var docId = doc.QuerySelector($"#{id}");
            Assert.NotNull(docId);
        }

        public static void AssessmentPanels(IHtmlDocument doc, List<List<string>> data)
        {
            foreach (var assessment in data)
            {
                Link(doc.GetElementById($"edit-assessment-{assessment[0].ToLower()}"), assessment[1],
                    $"https://localhost/Assessment/CreateEdit?assessmentId={assessment[0]}");
                Link(doc.GetElementById($"manage-test-material-{assessment[0].ToLower()}"), "Manage test material",
                    $"https://localhost/Assessment/TestMaterial?assessmentId={assessment[0]}");
                Link(doc.GetElementById($"manage-activities-{assessment[0].ToLower()}"),
                    "Manage activities and download expert review feedback",
                    $"https://localhost/Activity?assessmentId={assessment[0]}");
            }
        }
        
        public static void ActivityPanels(IHtmlDocument doc, List<List<string>> data)
        {
            foreach (var activity in data)
            {
                TextEqual(doc.GetElementById($"admin-{activity[0].ToLower()}"), "Administration material allocation");
                TextEqual(doc.GetElementById($"school-{activity[0].ToLower()}"), "Schools and form group allocation");
                TextEqual(doc.GetElementById($"publish-{activity[0].ToLower()}"), "Publish activity");
            }
        }

        public static void Checkboxes(IElement parent, params (string name, string value, string label)[] options)
        {
            var index = 0;
            foreach (var radioItem in parent.Descendents<IElement>()
                .Where(c => c.ClassList.Contains("govuk-checkboxes__item")))
            {
                var (name, value, label) = options[index];
                var inputElement = Assert.IsAssignableFrom<IHtmlInputElement>(radioItem.Children[0]);
                var labelElement = Assert.IsAssignableFrom<IHtmlLabelElement>(radioItem.Children[1]);

                ValidateInputElement(name, value, inputElement);
                ValidateLabelElement(label, labelElement);

                Assert.Equal(inputElement.Id, labelElement.HtmlFor);
                index++;
            }

            void ValidateInputElement(string name, string value, IHtmlInputElement e)
            {
                Assert.Equal("checkbox", e.Type);
                Assert.Equal(name, e.Name);
                Assert.Equal(value, e.Value, ignoreCase: true);
                Assert.True(e.ClassList.Contains("govuk-checkboxes__input"),
                    "A radio input should have the 'govuk-checkboxes__input' class applied");
            }

            void ValidateLabelElement(string label, IHtmlLabelElement e)
            {
                Assert.Equal(label, e.TextContent.Trim());
                Assert.True(e.ClassList.Contains("govuk-label") && e.ClassList.Contains("govuk-checkboxes__label"),
                    "A radio input should have the 'govuk-label' and 'govuk-checkboxes__label' classes applied");
            }
        }

        public static void ContainsFieldValidationError(IHtmlDocument doc, string message)
        {
            var errors = doc.GetElementsByClassName("field-validation-error");
            Assert.Contains(errors, x => x.InnerHtml == message);
        }

        public static void Details(IElement element, bool isOpen, string summaryText)
        {
            //Assert whether the details component is open or not
            var attributes = element.Attributes.Select(x => x.Name);
            
            if (isOpen)
            {
                Assert.Contains("open", attributes);
            }
            else if (isOpen == false)
            {
                Assert.DoesNotContain("open", attributes);
            }

            var actualSummarySpanText = element.Descendents<IElement>().Single(x => x.NodeName == "SPAN").TextContent;
            Assert.Contains(summaryText, actualSummarySpanText);
        }
    }