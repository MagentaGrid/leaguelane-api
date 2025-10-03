import Link from "next/link"

interface MatchCardProps {
  home: string
  away: string
  datetime: string
  homeLogoUrl?: string
  awayLogoUrl?: string
}

export function MatchCard({ home, away, datetime, homeLogoUrl, awayLogoUrl }: MatchCardProps) {
  const [day, time] = datetime.split(" ")

  const matchSlug = `${home.toLowerCase().replace(/\s+/g, "-")}-vs-${away.toLowerCase().replace(/\s+/g, "-")}`

  return (
    <Link href={`/match/${matchSlug}`}>
      <div className="p-4 relative border-b border-gray-200 hover:bg-gray-50 cursor-pointer transition-colors">
        <div className="flex items-center">
          <div className="w-[70%] space-y-3 pr-4">
            <div className="flex items-center gap-3">
              <div className="w-10 h-10 rounded-sm overflow-hidden bg-gray-100 flex items-center justify-center">
                <img
                  src={homeLogoUrl || `/placeholder.svg?height=40&width=40&query=${home} logo`}
                  alt={home}
                  className="w-full h-full object-cover"
                />
              </div>
              <span className="text-lg text-gray-900">{home}</span>
            </div>
            <div className="flex items-center gap-3">
              <div className="w-10 h-10 rounded-sm overflow-hidden bg-gray-100 flex items-center justify-center">
                <img
                  src={awayLogoUrl || `/placeholder.svg?height=40&width=40&query=${away} logo`}
                  alt={away}
                  className="w-full h-full object-cover"
                />
              </div>
              <span className="text-lg text-gray-900">{away}</span>
            </div>
          </div>
          <div className="absolute left-[70%] top-[10%] bottom-[10%] w-px bg-gray-200"></div>
          <div className="w-[30%] text-right pl-4">
            <div className="text-sm text-gray-500">{day}</div>
            <div className="text-lg font-medium text-gray-700">{time}</div>
          </div>
        </div>
      </div>
    </Link>
  )
}
