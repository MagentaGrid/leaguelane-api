export function TopLeagues() {
  return (
    <div className="w-80 flex-shrink-0">
      <div className="bg-white rounded-lg shadow-md overflow-hidden">
        {/* Header with light blue background */}
        <div className="bg-blue-100 px-4 py-3 flex items-center gap-2">
          <div className="w-5 h-5 flex items-center justify-center">
            <span className="text-yellow-600 font-bold">💡</span>
          </div>
          <h2 className="text-lg font-bold text-gray-800">Top leagues</h2>
        </div>

        {/* League Rows with separators */}
        <div className="divide-y divide-gray-200">
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">⚽</span>
            <span className="text-gray-700 font-medium">Premier League</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">⭐</span>
            <span className="text-gray-700 font-medium">Champions League</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">🇪🇸</span>
            <span className="text-gray-700 font-medium">LaLiga</span>
          </div>
          {/* FIFA World Cup with highlighted background */}
          <div className="flex items-center gap-3 px-4 py-3 bg-yellow-50 hover:bg-yellow-100 cursor-pointer transition-colors">
            <span className="text-lg">🏆</span>
            <span className="text-gray-700 font-medium">FIFA World Cup</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">🇩🇪</span>
            <span className="text-gray-700 font-medium">Bundesliga</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">🔷</span>
            <span className="text-gray-700 font-medium">MLS</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">⚽</span>
            <span className="text-gray-700 font-medium">Serie A</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">🏆</span>
            <span className="text-gray-700 font-medium">Europa League</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">🏆</span>
            <span className="text-gray-700 font-medium">Ligue 1</span>
          </div>
          <div className="flex items-center gap-3 px-4 py-3 hover:bg-gray-50 cursor-pointer transition-colors">
            <span className="text-lg">🇲🇽</span>
            <span className="text-gray-700 font-medium">Liga MX</span>
          </div>
        </div>
      </div>
    </div>
  )
}
