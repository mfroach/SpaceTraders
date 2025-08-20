export let marketCache: object[] = $state([]);
import type { Market } from './types'

export function getCachedMarket(marketId: number) {
  return marketCache[marketId];
}

export function setCachedMarket(marketId: number, data: object) {
  marketCache[marketId] = {
    data,
    timestamp: Date.now()
  };
}

export async function getMarket(systemSymbol: string, waypointSymbol: string) {
    const response = await fetch(
        `http://localhost:5247/systems/${systemSymbol}/waypoints/${waypointSymbol}/market`,
        { method: 'GET' });
    const marketData: Market = await response.json();
    return { marketData: marketData };
}