import type { PageLoad } from './$types';
import * as GetWaypoint from '$lib/GetWaypoint';

export const load: PageLoad = (async ({params}) => {
        const result = await GetWaypoint.getWaypoint(params.systemSymbol, params.waypointSymbol);
        return { result };
}) satisfies PageLoad;