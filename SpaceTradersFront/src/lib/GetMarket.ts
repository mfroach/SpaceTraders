import type { Market } from '$lib/types';
export async function getWaypoint(systemSymbol: string, waypointSymbol: string) {
    const response = await fetch(
        `http://localhost:5247/systems/${systemSymbol}/waypoints/${waypointSymbol}/market`,
        { method: 'GET' });
    const marketData: Market = await response.json();

    return { result: marketData };
}