import type { Article } from "../types/article"

export async function getArticles(): Promise<Article[]> {
  try {
    console.log("[v0] API endpoint not configured, using fallback data")
    return getFallbackArticles()

    // TODO: Uncomment this when API endpoint is ready
    /*
    const apiUrl = process.env.NEXT_PUBLIC_API_URL
    if (!apiUrl) {
      console.log('[v0] NEXT_PUBLIC_API_URL not configured, using fallback data')
      return getFallbackArticles()
    }

    const response = await fetch(`${apiUrl}/articles`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (!response.ok) {
      console.log('[v0] API response not ok:', response.status, 'using fallback data')
      return getFallbackArticles()
    }

    const data = await response.json()
    
    if (!data.isSuccess) {
      console.error('API Error:', data.errorMessage)
      return getFallbackArticles()
    }

    return data.Articles.slice(0, 2)
    */
  } catch (error) {
    console.error("[v0] Failed to fetch articles:", error)
    return getFallbackArticles()
  }
}

function getFallbackArticles(): Article[] {
  return [
    {
      id: 1,
      title: "Denzel Dumfries at a Crossroads: Will a Release Clause Spark a Transfer Frenzy?",
      date: "July 6, 2024",
      image: "/denzel-dumfries-football-transfer-news.jpg",
      url: "/articles/denzel-dumfries-transfer-analysis",
      excerpt:
        "Inter Milan's Dutch defender faces a crucial decision as his release clause becomes active this summer.",
    },
    {
      id: 2,
      title: "Senny Mayulu: PSG's Midfield Prodigy Ready to Shine in 2025/26",
      date: "July 6, 2024",
      image: "/senny-mayulu-psg-midfield-player.jpg",
      url: "/articles/senny-mayulu-psg-breakthrough",
      excerpt:
        "The young French midfielder is set to make his mark in PSG's first team after impressive youth performances.",
    },
  ]
}
