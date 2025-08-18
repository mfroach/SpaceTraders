import type {Agent} from '$lib/types';
export async function getAgent() {
	const response = await fetch('http://localhost:5247/my/agent', { method: 'GET' });
	const agentData: Agent = await response.json();
	return { agentData };
}
