import type { PageLoad } from './$types';
import * as GetMarket from '$lib/GetMarket';
export const load = (async ({params}) => {
        const result = await GetMarket.getWaypoint(params.systemSymbol, params.waypointSymbol);
        return { result };}) satisfies PageLoad;