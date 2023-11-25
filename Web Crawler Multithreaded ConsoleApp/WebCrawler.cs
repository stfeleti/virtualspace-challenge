using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Threading;

/// <summary>
/// Represents a simple multi-threaded web crawler.
/// </summary>
public class WebCrawler
{
    private HashSet<string> _visitedUrls = new HashSet<string>();
    private ConcurrentQueue<string> _urlQueue = new ConcurrentQueue<string>();
    private string _baseHost;
    private HttpClient _client = new HttpClient();
    private const int MaxRetries = 3;
    private const int DelayBetweenRequests = 1000; // milliseconds

    /// <summary>
    /// Initializes a new instance of the <see cref="WebCrawler"/> class.
    /// </summary>
    /// <param name="startUrl">The starting URL for the web crawler.</param>
    public WebCrawler(string startUrl)
    {
        _baseHost = new Uri(startUrl).Host; // Extract the hostname from the starting URL
        EnqueueInitialUrl(startUrl);        // Enqueue the starting URL
    }

    /// <summary>
    /// Enqueues the initial URL to start the crawling process.
    /// </summary>
    /// <param name="url">The URL to enqueue.</param>
    private void EnqueueInitialUrl(string url)
    {
        _urlQueue.Enqueue(url); // Add the start URL to the queue
    }

    /// <summary>
    /// Asynchronously performs the web crawling operation.
    /// </summary>
    public async Task CrawlAsync()
    {
        while (_urlQueue.TryDequeue(out string currentUrl)) // Continuously process URLs from the queue
        {
            if (!ShouldCrawlUrl(currentUrl)) // Check if the URL should be crawled
                continue;

            _visitedUrls.Add(currentUrl); // Mark the URL as visited

            try
            {
                await Task.Delay(DelayBetweenRequests); // Delay to be polite to the server
                string html = await GetHtmlWithRetries(currentUrl); // Fetch HTML with retries
                if (html == null) continue; // If fetching failed, skip to the next URL

                ProcessHtmlContent(html); // Process the HTML content to find links
            }
            catch
            {
                // Handle exceptions or log errors
            }
        }
    }

    /// <summary>
    /// Determines whether the specified URL should be crawled.
    /// </summary>
    /// <param name="url">The URL to check.</param>
    /// <returns><c>true</c> if the URL should be crawled; otherwise, <c>false</c>.</returns>
    private bool ShouldCrawlUrl(string url)
    {
        return !_visitedUrls.Contains(url); // Returns true if the URL has not been visited
    }

    /// <summary>
    /// Processes the HTML content of a web page and extracts links.
    /// </summary>
    /// <param name="html">The HTML content of the web page.</param>
    private void ProcessHtmlContent(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html); // Load the HTML content into the parser

        foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
        {
            string href = link.GetAttributeValue("href", string.Empty); // Extract the href value
            if (Uri.TryCreate(href, UriKind.Absolute, out Uri result) && result.Host == _baseHost) // Ensure it's an absolute URL and matches the base host
            {
                _urlQueue.Enqueue(result.ToString()); // Enqueue the new URL
            }
        }
    }

    /// <summary>
    /// Fetches HTML content from a URL with retries in case of failures.
    /// </summary>
    /// <param name="url">The URL to fetch.</param>
    /// <returns>The HTML content as a string, or <c>null</c> if the request fails.</returns>
    private async Task<string> GetHtmlWithRetries(string url)
    {
        for (int i = 0; i < MaxRetries; i++) // Try up to MaxRetries times
        {
            try
            {
                return await _client.GetStringAsync(url); // Attempt to fetch the HTML content
            }
            catch (HttpRequestException)
            {
                await Task.Delay(DelayBetweenRequests); // Delay before retrying
            }
        }
        return null; // Return null if all retries fail
    }

    /// <summary>
    /// Initiates the crawling process using a specified number of concurrent threads.
    /// </summary>
    /// <param name="numberOfThreads">The number of threads to use for crawling.</param>
    public void Run(int numberOfThreads)
    {
        List<Task> tasks = new List<Task>();

        for (int i = 0; i < numberOfThreads; i++) // Create tasks for each thread
        {
            tasks.Add(Task.Run(() => CrawlAsync()));
        }

        Task.WhenAll(tasks).Wait(); // Wait for all tasks to complete

        OutputVisitedUrls(); // Output the list of visited URLs
    }

    /// <summary>
    /// Outputs the visited URLs to the console.
    /// </summary>
    private void OutputVisitedUrls()
    {
        foreach (var url in _visitedUrls) // Iterate through each visited URL
        {
            Console.WriteLine(url); // Print the URL to the console
        }
    }
}

