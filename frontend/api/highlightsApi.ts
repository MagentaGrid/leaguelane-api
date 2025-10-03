import type { Highlight } from "../types/highlight"

export async function getHighlights(): Promise<Highlight[]> {
  try {
    console.log("[v0] API endpoint not configured, using fallback data")
    return getFallbackHighlights()

    // TODO: Uncomment this when API endpoint is ready
    /*
    const apiUrl = process.env.NEXT_PUBLIC_API_URL
    if (!apiUrl) {
      console.log('[v0] NEXT_PUBLIC_API_URL not configured, using fallback data')
      return getFallbackHighlights()
    }

    const response = await fetch(`${apiUrl}/highlights`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (!response.ok) {
      console.log('[v0] API response not ok:', response.status, 'using fallback data')
      return getFallbackHighlights()
    }

    const data = await response.json()
    
    if (!data.isSuccess) {
      console.error('API Error:', data.errorMessage)
      return getFallbackHighlights()
    }

    return data.Highlights
    */
  } catch (error) {
    console.error("[v0] Failed to fetch highlights:", error)
    return getFallbackHighlights()
  }
}

function getFallbackHighlights(): Highlight[] {
  return [
    {
      id: 1,
      title: "LeagueLane: The Best Football Previews & Betting Tips",
      content:
        "LeagueLane is a community of like-minded enthusiasts who are extremely passionate about football. It is also an art that needs to be treated carefully and one that delivers more emotion than you could possibly imagine. It's the beautiful game for a reason and we have made it our goal to provide the masses with football previews of things they are most passionate about and the most accurate betting predictions.",
      type: "intro",
    },
    {
      id: 2,
      title: "What We Offer",
      content:
        "Here at LeagueLane we work hard to track only the very best previews and recommendations available. We have articles for nearly every match, from a wide range of leagues from Premier League predictions to Europa's elite Champions League. We also cover every angle where you'll find each of a breakdown into the finer details of what we offer.",
      type: "feature",
    },
  ]
}
