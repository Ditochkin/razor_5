using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace RazorPagesGeneral
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }


    [Serializable]
    public class Testimonial
    {
        public string? CommentLabel { get; set; }
        public string? Comment { get; set; }
        public string? Name { get; set; }
        public string? JobTitle { get; set; }
        public string? ImageUrl { get; set; }
    }
    public interface ITestimonialService
    {
        IEnumerable<Testimonial> getAll();
    }

    public class TestimonialService : ITestimonialService
    {
        public IEnumerable<Testimonial> getAll()
        {
            var streamReader = new StreamReader("testimonials.json");

            string json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<Testimonial[]>(json) ?? new Testimonial[] { };
        }
    }
    /// <summary>
    /// /////////////////////////////////////////////////////////////////////////////
    /// </summary>

    public class Feature
    {
        public string? Delay { get; set; }
        public string? Class { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    public interface IFeatureService
    {
        IEnumerable<Feature> getAll();
    }

    public class FeatureService : IFeatureService
    {
        public IEnumerable<Feature> getAll()
        {
            var streamReader = new StreamReader("features.json");

            string json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<Feature[]>(json) ?? new Feature[] { };
        }
    }

    public class Contact
    {
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String email { get; set; }
        public String phone { get; set; }
        public String select_service { get; set; }
        public String select_price { get; set; }
        public String comments { get; set; }

        public Contact(string first_name, string last_name, string email, string phone, string select_service, string select_price, string comments)
        {
            this.first_name = first_name;
            this.last_name = last_name;
            this.email = email;
            this.phone = phone;
            this.select_service = select_service;
            this.select_price = select_price;
            this.comments = comments;
        }
    }

    public interface IContactsService
    {
        void writeContact(Contact contact);
    }

    public class ContactsService : IContactsService
    {
        public void writeContact(Contact contact)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            String contactFile = @"storage\contacts.csv";
            using (FileStream stream = File.Open(contactFile, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))

            using (CsvWriter csvWriter = new CsvWriter(writer, config))
            {
                csvWriter.WriteRecord<Contact>(contact);
                csvWriter.NextRecord();
            }
        }
    }
}
