import type { BestBet } from "../types/bestBet"

export async function getBestBets(): Promise<BestBet[]> {
  try {
    console.log("[v0] API endpoint not configured, using fallback data")
    return getFallbackBestBets()

    // TODO: Uncomment this when API endpoint is ready
    /*
    const apiUrl = process.env.NEXT_PUBLIC_API_URL
    if (!apiUrl) {
      console.log('[v0] NEXT_PUBLIC_API_URL not configured, using fallback data')
      return getFallbackBestBets()
    }

    const response = await fetch(`${apiUrl}/best-bets`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (!response.ok) {
      console.log('[v0] API response not ok:', response.status, 'using fallback data')
      return getFallbackBestBets()
    }

    const data = await response.json()
    
    if (!data.isSuccess) {
      console.error('API Error:', data.errorMessage)
      return getFallbackBestBets()
    }

    return data.BestBets.slice(0, 2)
    */
  } catch (error) {
    console.error("[v0] Failed to fetch best bets:", error)
    return getFallbackBestBets()
  }
}

function getFallbackBestBets(): BestBet[] {
  return [
    {
      id: 1,
      title: "Denzel Dumfries at a Crossroads: Will a Release Clause Spark a Transfer Frenzy?",
      date: "July 6, 2024",
      image: "/denzel-dumfries-football-player-action-shot.jpg",
      url: "/best-bets/denzel-dumfries-transfer-analysis",
    },
    {
      id: 2,
      title: "Senny Mayulu: PSG's Midfield Prodigy Ready to Shine in 2025/26",
      date: "July 6, 2024",
      image: "/senny-mayulu-psg-football-player.jpg",
      url: "/best-bets/senny-mayulu-psg-analysis",
    },
  ]
}
