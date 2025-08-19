import type { PageLoad } from './$types';
import * as getAgent from '$lib/GetAgent';
export const load: PageLoad = (async () => {
        const result = await getAgent.getAgent();
        return { result };
}) satisfies PageLoad;