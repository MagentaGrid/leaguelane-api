import Image from "next/image"
import {
  Trophy,
  Star,
  Crown,
  Target,
  RotateCcw,
  CheckCircle,
  ArrowLeftRight,
  Plus,
  BarChart3,
  User,
} from "lucide-react"

const leagues = [
  {
    name: "Premier League",
    logo: "/premier-league-logo.jpg",
  },
  {
    name: "Champions league",
    logo: "/generic-soccer-tournament-logo.png",
  },
  {
    name: "FIFA World Cup",
    logo: "/fifa-world-cup-logo.jpg",
  },
  {
    name: "Bundesliga",
    logo: "/bundesliga-logo.png",
  },
  {
    name: "La Liga",
    logo: "/la-liga-logo.png",
  },
  {
    name: "Serie A",
    logo: "/serie-a-logo.png",
  },
  {
    name: "Europa League",
    logo: "/europa-league-logo.png",
  },
  {
    name: "Ligue 1",
    logo: "/ligue-1-logo.png",
  },
  {
    name: "Liga MX",
    logo: "/liga-mx-logo.jpg",
  },
]

const bettingTips = [
  { name: "All Tips", icon: Star, active: true },
  { name: "Premium VIP Tips", icon: Crown },
  { name: "Tip of the Day", icon: Target },
  { name: "Daily Accumulator", icon: RotateCcw },
  { name: "Correct Score Tips", icon: CheckCircle },
  { name: "BTTS & Win Double", icon: ArrowLeftRight },
  { name: "Over 2.5 goals/Accumulators", icon: Plus },
  { name: "Both Teams to Score Acca", icon: BarChart3 },
  { name: "Anytime Goalscorer Tips", icon: User },
]

export function TopLeagues() {
  return (
    <aside className="lg:col-span-1 space-y-6">
      {/* Top Leagues Section */}
      <div className="bg-card p-4 rounded-xl shadow-sm">
        <h3 className="font-bold text-lg mb-4 text-primary flex items-center gap-2">
          <Trophy className="w-5 h-5 text-primary" />
          Top Leagues
        </h3>
        <ul className="space-y-3">
          {leagues.map((league) => (
            <li key={league.name}>
              <a href="#" className="flex items-center gap-3 text-foreground hover:text-primary transition-colors">
                <Image
                  alt={`${league.name} logo`}
                  className="w-5 h-5 object-contain"
                  src={league.logo || "/placeholder.svg"}
                  width={20}
                  height={20}
                />
                {league.name}
              </a>
            </li>
          ))}
        </ul>
      </div>

      {/* Betting Tips Section */}
      <div className="bg-card p-4 rounded-xl shadow-sm">
        <ul className="space-y-4">
          {bettingTips.map((tip) => {
            const IconComponent = tip.icon
            return (
              <li key={tip.name}>
                <a
                  href="#"
                  className={`flex items-center gap-3 transition-colors ${
                    tip.active ? "font-semibold text-primary" : "text-foreground hover:text-primary"
                  }`}
                >
                  <IconComponent className="w-4 h-4" />
                  {tip.name}
                </a>
              </li>
            )
          })}
        </ul>
      </div>
    </aside>
  )
}
