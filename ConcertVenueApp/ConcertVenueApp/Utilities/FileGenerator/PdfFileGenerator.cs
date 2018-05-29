using ConcertVenueApp.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConcertVenueApp.Utilities.FileGenerator
{
    public static class PdfFileGenerator
    {
        public static FileStream GeneratePdfTickets(List<Ticket> tickets)
        {
            MemoryStream mem = new MemoryStream();
            Document doc = new Document();
            PdfWriter.GetInstance(doc, mem).CloseStream = false;

            doc.Open();
            doc.Add(new Paragraph("Your tickets are here:"));
            doc.Add(new Paragraph(""));
            foreach(var ticket in tickets)
            {
                doc.Add(new Paragraph(String.Format("Ticket no. {0} - event: {1} - date: {2} - price: {3} ",ticket.GetId(),ticket.GetTicketEvent().GetTitle(), ticket.GetTicketEvent().GetDate().ToString("dd-MM-yyyy"), ticket.GetTicketEvent().GetTicketPrice())));
            }
            doc.Close();
            FileStream file = new FileStream("tickets.pdf", FileMode.Create, FileAccess.ReadWrite);
            mem.WriteTo(file);
            file.Close();
            return file;
        }
    }
}
