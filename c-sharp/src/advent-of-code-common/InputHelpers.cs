using System.Net;

namespace AdventOfCode.Common;

public static class Tools
{
	private static Uri AoCUrl { get; } = new("https://adventofcode.com/");

	public static async Task GetInput(int year, int day, string outputPath)
	{
		// If the file doesn't exist we need to request and create it from the server
		if (File.Exists(outputPath)) return;
		await RequestInput(year, day, outputPath);
	}

	/// <summary>
	///     Makes a request using your session cookie to get the given input and store it in a file for future runs
	/// </summary>
	/// <param name="year">2023 for this year</param>
	/// <param name="day">2 for day 2</param>
	/// <param name="path">Path of the file we want to create</param>
	private static async Task RequestInput(int year, int day, string path)
	{
		// The session_cookie.txt file has to be placed in your project directory
		// Make sure to add the file to .gitignore so you won't share it with the world
		var session = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "session_cookie.txt"));
		var cookies = new CookieContainer();
		cookies.Add(AoCUrl, new Cookie("session", session));

		await using var file = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
		using var handler = new HttpClientHandler();
		handler.CookieContainer = cookies;
		using var client = new HttpClient(handler);
		client.BaseAddress = AoCUrl;
		using var response = await client.GetAsync($"{year}/day/{day}/input");
		await using var stream = await response.Content.ReadAsStreamAsync();
		await stream.CopyToAsync(file);
	}
}
