import type { Offer } from "../types/offer"

export async function getOffers(): Promise<Offer[]> {
  try {
    console.log("[v0] API endpoint not configured, using fallback data")
    return getFallbackOffers()

    // TODO: Uncomment this when API endpoint is ready
    /*
    const apiUrl = process.env.NEXT_PUBLIC_API_URL
    if (!apiUrl) {
      console.log('[v0] NEXT_PUBLIC_API_URL not configured, using fallback data')
      return getFallbackOffers()
    }

    const response = await fetch(`${apiUrl}/offers`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (!response.ok) {
      console.log('[v0] API response not ok:', response.status, 'using fallback data')
      return getFallbackOffers()
    }

    const data = await response.json()
    
    if (!data.isSuccess) {
      console.error('API Error:', data.errorMessage)
      return getFallbackOffers()
    }

    return data.Offers.slice(0, 3)
    */
  } catch (error) {
    console.error("[v0] Failed to fetch offers:", error)
    return getFallbackOffers()
  }
}

function getFallbackOffers(): Offer[] {
  return [
    {
      id: 1,
      header: "Welcome Bonus",
      description:
        "Get 100% match bonus up to $500 on your first deposit. Start your betting journey with extra funds!",
      url: "/welcome-bonus",
    },
    {
      id: 2,
      header: "Free Bet Friday",
      description:
        "Every Friday get a $25 free bet when you place 5 or more bets during the week. No strings attached!",
      url: "/free-bet-friday",
    },
    {
      id: 3,
      header: "Casino Bonus",
      description: "200% welcome bonus up to $1000 plus 50 free spins on selected slot games. Play and win big!",
      url: "/casino-bonus",
    },
  ]
}
