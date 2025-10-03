import type { Prediction } from "../types/predictions"

export async function getPredictions(): Promise<Prediction[]> {
  try {
    console.log("[v0] API endpoint not configured, using fallback data")
    return getFallbackPredictions()

    // TODO: Uncomment this when API endpoint is ready
    /*
    const apiUrl = process.env.NEXT_PUBLIC_API_URL
    if (!apiUrl) {
      console.log('[v0] NEXT_PUBLIC_API_URL not configured, using fallback data')
      return getFallbackPredictions()
    }

    const response = await fetch(`${apiUrl}/predictions`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    if (!response.ok) {
      console.log('[v0] API response not ok:', response.status, 'using fallback data')
      return getFallbackPredictions()
    }

    const data = await response.json()
    
    if (!data.isSuccess) {
      console.error('API Error:', data.errorMessage)
      return getFallbackPredictions()
    }

    return data.predictions
    */
  } catch (error) {
    console.error("[v0] Failed to fetch predictions:", error)
    return getFallbackPredictions()
  }
}

function getFallbackPredictions(): Prediction[] {
  return [
    {
      home: {
        Team: "AC Oulu",
        LogoUrl: "/.jpg?key=kup0t&height=24&width=24&query=AC%20Oulu%20football%20team%20logo",
      },
      away: {
        Team: "Brann",
        LogoUrl: "/.jpg?key=8xqok&height=24&width=24&query=Brann%20football%20team%20logo",
      },
      Time: "16:30",
      Day: "Sun",
    },
    {
      home: {
        Team: "KTP",
        LogoUrl: "/.jpg?key=u8usg&height=24&width=24&query=KTP%20football%20team%20logo",
      },
      away: {
        Team: "Viking",
        LogoUrl: "/.jpg?key=5tvva&height=24&width=24&query=Viking%20football%20team%20logo",
      },
      Time: "16:30",
      Day: "Sun",
    },
    {
      home: {
        Team: "Barcelona",
        LogoUrl: "/.jpg?key=bar1&height=24&width=24&query=Barcelona%20football%20team%20logo",
      },
      away: {
        Team: "Real Madrid",
        LogoUrl: "/.jpg?key=real1&height=24&width=24&query=Real%20Madrid%20football%20team%20logo",
      },
      Time: "18:00",
      Day: "Mon",
    },
    {
      home: {
        Team: "Liverpool",
        LogoUrl: "/.jpg?key=liv1&height=24&width=24&query=Liverpool%20football%20team%20logo",
      },
      away: {
        Team: "Arsenal",
        LogoUrl: "/.jpg?key=ars1&height=24&width=24&query=Arsenal%20football%20team%20logo",
      },
      Time: "18:00",
      Day: "Mon",
    },
    {
      home: {
        Team: "Bayern Munich",
        LogoUrl: "/.jpg?key=bay1&height=24&width=24&query=Bayern%20Munich%20football%20team%20logo",
      },
      away: {
        Team: "Dortmund",
        LogoUrl: "/.jpg?key=dor1&height=24&width=24&query=Dortmund%20football%20team%20logo",
      },
      Time: "20:00",
      Day: "Tue",
    },
    {
      home: {
        Team: "PSG",
        LogoUrl: "/.jpg?key=psg1&height=24&width=24&query=PSG%20football%20team%20logo",
      },
      away: {
        Team: "Marseille",
        LogoUrl: "/.jpg?key=mar1&height=24&width=24&query=Marseille%20football%20team%20logo",
      },
      Time: "20:00",
      Day: "Tue",
    },
  ]
}
