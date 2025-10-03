export type Prediction = {
  home: {
    Team: string
    LogoUrl: string
  }
  away: {
    Team: string
    LogoUrl: string
  }
  Time: string // e.g., "16:30"
  Day: string // e.g., "Sun"
}

export type PredictionsResponse = {
  isSuccess: boolean
  errorMessage?: string
  predictions: Prediction[]
}

// Legacy type for backward compatibility
export type FeaturedPrediction = {
  home: string
  away: string
  datetime: string
}

export type FeaturedPredictionsResponse = {
  isSuccess: boolean
  errorMessage?: string
  FeaturedPrediction: FeaturedPrediction[]
}
