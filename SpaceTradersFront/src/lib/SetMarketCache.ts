import type { Market } from '$lib/types';
import * as Cache from '$lib/cache.svelte';
export async function cacheMarket(systemSymbol: string, waypointSymbol: string) {
    const response = await fetch(
        `http://localhost:5247/systems/${systemSymbol}/waypoints/${waypointSymbol}/market`,
        { method: 'GET' });
    const marketData: Market = await response.json();
    Cache.setCachedMarket(1, marketData);
    return {};
}