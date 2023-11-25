//string startUrl = "https://webscraper.io/test-sites;
string startUrl = "https://crawler-test.com/";
WebCrawler crawler = new WebCrawler(startUrl);
crawler.Run(5); // You can adjust the number of threads