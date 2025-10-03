import Link from "next/link"
import Image from "next/image"
import { Button } from "@/components/ui/button"
import { Award } from "lucide-react"
import type { Prediction } from "@/types/predictions"

interface PredictionCardsProps {
  predictions: Prediction[]
}

export function PredictionCards({ predictions }: PredictionCardsProps) {
  return (
    <div className="bg-card p-6 rounded-xl shadow-sm">
      <h3 className="font-bold text-xl mb-4 flex items-center gap-2">
        <Award className="w-5 h-5 text-primary" />
        <span className="text-primary">Featured Predictions</span>
      </h3>

      <div className="space-y-0">
        {Array.from({ length: 3 }, (_, rowIndex) => {
          const match1 = predictions[rowIndex * 2]
          const match2 = predictions[rowIndex * 2 + 1]

          return (
            <div key={rowIndex} className="border-b border-border pb-4 mb-4 last:border-b-0 last:mb-0 last:pb-0">
              <div className="flex items-center gap-4">
                {/* First match */}
                {match1 && (
                  <div className="flex-1">
                    <Link
                      href={`/match/${match1.home.Team.toLowerCase().replace(/\s+/g, "-")}-vs-${match1.away.Team.toLowerCase().replace(/\s+/g, "-")}`}
                    >
                      <div className="grid grid-cols-3 items-center p-3 rounded-lg hover:bg-muted/50 transition-colors cursor-pointer">
                        <div className="flex items-center gap-3">
                          <Image
                            alt={`${match1.home.Team} logo`}
                            className="w-6 h-6 rounded-full"
                            src={match1.home.LogoUrl || "/placeholder.svg"}
                            width={24}
                            height={24}
                          />
                          <span className="font-medium text-foreground">{match1.home.Team}</span>
                        </div>
                        <div className="text-center text-muted-foreground">
                          <div className="font-semibold text-foreground">{match1.Time}</div>
                          <div className="text-xs">{match1.Day}</div>
                        </div>
                        <div className="flex items-center gap-3 justify-end">
                          <span className="font-medium text-foreground">{match1.away.Team}</span>
                          <Image
                            alt={`${match1.away.Team} logo`}
                            className="w-6 h-6 rounded-full"
                            src={match1.away.LogoUrl || "/placeholder.svg"}
                            width={24}
                            height={24}
                          />
                        </div>
                      </div>
                    </Link>
                  </div>
                )}

                {/* Divider between matches */}
                {match1 && match2 && <div className="w-px h-16 bg-border"></div>}

                {/* Second match */}
                {match2 && (
                  <div className="flex-1">
                    <Link
                      href={`/match/${match2.home.Team.toLowerCase().replace(/\s+/g, "-")}-vs-${match2.away.Team.toLowerCase().replace(/\s+/g, "-")}`}
                    >
                      <div className="grid grid-cols-3 items-center p-3 rounded-lg hover:bg-muted/50 transition-colors cursor-pointer">
                        <div className="flex items-center gap-3">
                          <Image
                            alt={`${match2.home.Team} logo`}
                            className="w-6 h-6 rounded-full"
                            src={match2.home.LogoUrl || "/placeholder.svg"}
                            width={24}
                            height={24}
                          />
                          <span className="font-medium text-foreground">{match2.home.Team}</span>
                        </div>
                        <div className="text-center text-muted-foreground">
                          <div className="font-semibold text-foreground">{match2.Time}</div>
                          <div className="text-xs">{match2.Day}</div>
                        </div>
                        <div className="flex items-center gap-3 justify-end">
                          <span className="font-medium text-foreground">{match2.away.Team}</span>
                          <Image
                            alt={`${match2.away.Team} logo`}
                            className="w-6 h-6 rounded-full"
                            src={match2.away.LogoUrl || "/placeholder.svg"}
                            width={24}
                            height={24}
                          />
                        </div>
                      </div>
                    </Link>
                  </div>
                )}
              </div>
            </div>
          )
        })}
      </div>

      <div className="mt-6 text-center">
        <Button className="py-2 px-6 bg-primary text-primary-foreground rounded-lg font-semibold hover:bg-primary/90 transition-colors">
          View All Predictions
        </Button>
      </div>
    </div>
  )
}
