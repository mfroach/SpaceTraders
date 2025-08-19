import type { PageLoad } from './$types';
import * as Cache from '$lib/cache';
import * as SetCache from '$lib/SetMarketCache';
export const load = (async ({params}) => {
    await SetCache.cacheMarket(params.systemSymbol, params.waypointSymbol);
        const result = Cache.getCachedMarket(1);
        return { result };}) satisfies PageLoad;