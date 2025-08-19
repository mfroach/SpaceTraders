let marketCache: object[] = [];
import type { Market } from './types'
export function getCachedMarket(marketId: number) {
  return marketCache[marketId];
}

export function setCachedMarket(marketId: number, data: Market) {
  marketCache[marketId] = {
    data,
    timestamp: Date.now()
  };
}