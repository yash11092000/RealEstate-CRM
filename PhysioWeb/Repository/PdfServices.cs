using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Snippets.Font;
using MigraDoc;

namespace PhysioWeb.Repository
{
    public class PdfServices : IPdfServices
    {
        static PdfServices()
        {
            // This runs once per AppDomain before any instance is created
            GlobalFontSettings.FontResolver = new FailsafeFontResolver();

        }
        public byte[] GenerateQuotationPdf()
        {
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new FailsafeFontResolver();
            }
            Document document = new Document();
            document.Info.Title = "Real Estate Quotation";
            document.Styles["Normal"].Font.Name = "Calibri";
            document.Styles["Normal"].Font.Size = 11;
            document.Styles["Normal"].Font.Color = Colors.DarkGray;

            // 2. Add section with proper margins
            Section section = document.AddSection();
            section.PageSetup = document.DefaultPageSetup.Clone();
            section.PageSetup.LeftMargin = "2.5cm";
            section.PageSetup.RightMargin = "2.5cm";
            section.PageSetup.TopMargin = "2cm";
            section.PageSetup.BottomMargin = "2cm";

            // ========== HEADER SECTION ==========

            // Company Logo/Header
            Paragraph companyHeader = section.AddParagraph();
            companyHeader.AddText("PRIME PROPERTIES");
            companyHeader.Format.Font.Size = 20;
            companyHeader.Format.Font.Bold = true;
            companyHeader.Format.Font.Color = Colors.DarkBlue;
            companyHeader.Format.SpaceAfter = "0.3cm";
            companyHeader.Format.Alignment = ParagraphAlignment.Left;

            // Company Tagline
            Paragraph tagline = section.AddParagraph();
            tagline.AddText("Your Trusted Real Estate Partner");
            tagline.Format.Font.Size = 10;
            tagline.Format.Font.Italic = true;
            tagline.Format.Font.Color = Colors.Gray;
            tagline.Format.SpaceAfter = "1.5cm";

            // Main Title
            Paragraph title = section.AddParagraph("QUOTATION");
            title.Format.Font.Size = 24;
            title.Format.Font.Bold = true;
            title.Format.Font.Color = Colors.Navy;
            title.Format.SpaceAfter = "0.2cm";
            title.Format.Alignment = ParagraphAlignment.Center;

            // Quotation ID and Date
            Paragraph quoteInfo = section.AddParagraph();
            quoteInfo.AddText($"Quotation #: PQ-{DateTime.Now:yyyyMMdd}-001");
            quoteInfo.AddTab();
            quoteInfo.AddText($"Date: {DateTime.Now:dd-MMM-yyyy}");
            quoteInfo.Format.SpaceAfter = "1.5cm";
            quoteInfo.Format.Alignment = ParagraphAlignment.Center;

            // ========== CLIENT & PROPERTY DETAILS ==========

            // Section Title
            Paragraph detailsTitle = section.AddParagraph("Client & Property Details");
            detailsTitle.Format.Font.Size = 14;
            detailsTitle.Format.Font.Bold = true;
            detailsTitle.Format.Font.Color = Colors.DarkBlue;
            detailsTitle.Format.SpaceAfter = "0.5cm";
            detailsTitle.Format.Borders.Bottom.Width = 0.75;
            detailsTitle.Format.Borders.Bottom.Color = Colors.DarkBlue;

            // Create a 2-column table for details
            Table detailsTable = section.AddTable();
            detailsTable.Borders.Visible = false;
            detailsTable.Format.SpaceAfter = "1.5cm";

            // Two equal columns
            detailsTable.AddColumn("8cm");
            detailsTable.AddColumn("8cm");

            Row clientRow = detailsTable.AddRow();

            // Left Column - Client Details
            clientRow.Cells[0].AddParagraph("CLIENT INFORMATION");
            clientRow.Cells[0].Format.Font.Bold = true;
            clientRow.Cells[0].Format.Font.Size = 11;
            clientRow.Cells[0].Format.Shading.Color = Colors.LightGray;
            clientRow.Cells[0].Format.LeftIndent = "0.2cm";

            Row clientDetails = detailsTable.AddRow();
            clientDetails.Cells[0].AddParagraph("• Name: Mr. Rahul Sharma");
            clientDetails.Cells[0].AddParagraph("• Contact: +91 98765 43210");
            clientDetails.Cells[0].AddParagraph("• Email: rahul.sharma@email.com");
            clientDetails.Cells[0].Format.Font.Size = 10;
            clientDetails.Cells[0].Format.LeftIndent = "0.5cm";

            // Right Column - Property Details
            clientRow.Cells[1].AddParagraph("PROPERTY DETAILS");
            clientRow.Cells[1].Format.Font.Bold = true;
            clientRow.Cells[1].Format.Font.Size = 11;
            clientRow.Cells[1].Format.Shading.Color = Colors.LightGray;
            clientRow.Cells[1].Format.LeftIndent = "0.2cm";

            Row propertyDetails = detailsTable.AddRow();
            propertyDetails.Cells[1].AddParagraph("• Type: 2 BHK Luxury Apartment");
            propertyDetails.Cells[1].AddParagraph("• Location: Andheri East, Mumbai");
            propertyDetails.Cells[1].AddParagraph("• Project: Skyline Residences");
            propertyDetails.Cells[1].AddParagraph("• Area: 950 sq. ft.");
            propertyDetails.Cells[1].Format.Font.Size = 10;
            propertyDetails.Cells[1].Format.LeftIndent = "0.5cm";

            // ========== COST BREAKDOWN ==========

            Paragraph costTitle = section.AddParagraph("Cost Breakdown");
            costTitle.Format.Font.Size = 14;
            costTitle.Format.Font.Bold = true;
            costTitle.Format.Font.Color = Colors.DarkBlue;
            costTitle.Format.SpaceAfter = "0.5cm";
            costTitle.Format.Borders.Bottom.Width = 0.75;
            costTitle.Format.Borders.Bottom.Color = Colors.DarkBlue;

            // Main Cost Table
            Table costTable = section.AddTable();
            costTable.Borders.Width = 0.5;
            costTable.Borders.Color = Colors.Gray;
            costTable.Format.SpaceAfter = "1cm";

            // Column widths
            costTable.AddColumn("10cm"); // Description
            costTable.AddColumn("3cm");  // Qty
            costTable.AddColumn("4cm");  // Amount

            // Table Header
            Row costHeader = costTable.AddRow();
            costHeader.HeadingFormat = true;
            costHeader.Shading.Color = Colors.LightBlue;
            costHeader.Format.Font.Bold = true;
            costHeader.Format.Font.Color = Colors.White;
            costHeader.VerticalAlignment = VerticalAlignment.Center;

            costHeader.Cells[0].AddParagraph("DESCRIPTION");
            costHeader.Cells[1].AddParagraph("QTY");
            costHeader.Cells[2].AddParagraph("AMOUNT (₹)");

            // RUPEES SYMBOL FIX: Use Unicode character or text representation

            // Data Rows
            AddCostRow(costTable, "Property Base Price", "1", "75,00,000");
            AddCostRow(costTable, "Registration Charges", "-", "2,50,000");
            AddCostRow(costTable, "Stamp Duty @ 5%", "-", "3,75,000");
            AddCostRow(costTable, "Maintenance Deposit (1 Year)", "-", "60,000");
            AddCostRow(costTable, "Club Membership Charges", "-", "1,00,000");

            // Empty row for spacing
            Row spacer = costTable.AddRow();
            spacer.Borders.Visible = false;
            spacer.Height = "0.5cm";

            // Subtotal
            Row subtotal = costTable.AddRow();
            subtotal.Cells[0].AddParagraph("Subtotal");
            subtotal.Cells[0].Format.Font.Bold = true;
            subtotal.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            subtotal.Cells[0].MergeRight = 1;
            subtotal.Cells[2].AddParagraph("₹ 82,85,000");
            subtotal.Cells[2].Format.Font.Bold = true;

            // GST Row
            Row gstRow = costTable.AddRow();
            gstRow.Cells[0].AddParagraph("GST @ 18%");
            gstRow.Cells[0].Format.Alignment = ParagraphAlignment.Right;
            gstRow.Cells[0].MergeRight = 1;
            gstRow.Cells[2].AddParagraph("₹ 14,91,300");

            // Total Row (with styling)
            Row totalRow = costTable.AddRow();
            totalRow.Shading.Color = Colors.Navy;
            totalRow.Format.Font.Bold = true;
            totalRow.Format.Font.Color = Colors.White;
            totalRow.VerticalAlignment = VerticalAlignment.Center;
            totalRow.Height = "1cm";

            totalRow.Cells[0].AddParagraph("TOTAL AMOUNT");
            totalRow.Cells[0].Format.Font.Size = 12;
            totalRow.Cells[0].MergeRight = 1;
            totalRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;

            totalRow.Cells[2].AddParagraph("₹ 97,76,300");
            totalRow.Cells[2].Format.Font.Size = 12;
            totalRow.Cells[2].Format.Alignment = ParagraphAlignment.Center;

            // ========== TERMS & FOOTER ==========

            section.AddParagraph().Format.SpaceAfter = "1.5cm";

            Paragraph termsTitle = section.AddParagraph("Terms & Conditions");
            termsTitle.Format.Font.Size = 12;
            termsTitle.Format.Font.Bold = true;
            termsTitle.Format.SpaceAfter = "0.3cm";

            Paragraph terms = section.AddParagraph();
            terms.AddText("1. This quotation is valid for 30 days from the date of issue.\n");
            terms.AddText("2. Prices are subject to change without prior notice.\n");
            terms.AddText("3. Registration charges are approximate and may vary.\n");
            terms.AddText("4. 10% booking amount required to reserve the property.\n");
            terms.AddText("5. Balance payment as per construction-linked plan.");
            terms.Format.Font.Size = 10;
            terms.Format.Font.Color = Colors.DarkGray;
            terms.Format.SpaceAfter = "1.5cm";

            // Footer section
            Paragraph footer = section.AddParagraph();
            footer.AddText("For any queries, contact: sales@primeproperties.com | +91 22 1234 5678\n");
            footer.AddText("Office: Prime Properties, 101 Skyline Tower, Andheri East, Mumbai - 400069");
            footer.Format.Font.Size = 9;
            footer.Format.Font.Color = Colors.Gray;
            footer.Format.SpaceBefore = "1cm";
            footer.Format.Borders.Top.Width = 0.5;
            footer.Format.Borders.Top.Color = Colors.LightGray;
            footer.Format.Alignment = ParagraphAlignment.Center;

            // ========== RENDER PDF ==========

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);
            renderer.Document = document;
            renderer.RenderDocument();

            using MemoryStream stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);
            return stream.ToArray();
        }
        // Helper method for adding cost rows
        private void AddCostRow(Table table, string description, string qty, string amount)
        {
            Row row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].AddParagraph(description);
            row.Cells[1].AddParagraph(qty);
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;

            // Fix for rupee symbol: Use "₹" directly or fallback to "Rs."
            string amountWithSymbol = "₹ " + amount;
            row.Cells[2].AddParagraph(amountWithSymbol);
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
        }

    }
}
