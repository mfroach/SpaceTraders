import type { PageLoad } from './$types';

export const load: PageLoad = (async ({fetch}) => {
        const response = await fetch("http://localhost:5247/my/agent", {method: 'GET'});
        const result = await response.json();
        return { result };
}) satisfies PageLoad;