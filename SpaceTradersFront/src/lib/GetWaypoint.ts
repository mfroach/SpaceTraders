import type { Waypoint } from '$lib/types';
export async function getWaypoint(systemSymbol: string, waypointSymbol: string) {
    const response = await fetch(
        `http://localhost:5247/systems/${systemSymbol}/waypoints/${waypointSymbol}`,
        { method: 'GET' });
    const waypointData: Waypoint = await response.json();

    return { result: waypointData };
}