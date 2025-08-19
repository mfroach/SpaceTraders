export interface Agent {
    name: string;
    id: string;
    status: string;
}

export interface Waypoint {
    symbol: string;
    type: string;
    systemSymbol: string;
    x: number;
    y: number;
    orbitals: Orbital;
    orbits: string;
    isUnderConstruction: boolean;
}

export interface Orbital {
    symbol: string;
}

export interface Market {
    symbol:       string;
    exports:      ExchangeItem[];
    imports:      ExchangeItem[];
    exchange:     ExchangeItem[];
    transactions: Transaction[];
    tradeGoods:   TradeGood[];
}

export interface ExchangeItem {
    symbol:      string;
    name:        string;
    description: string;
}

export interface TradeGood {
    symbol:        string;
    type:          string;
    tradeVolume:   number;
    supply:        string;
    activity:      string;
    purchasePrice: number;
    sellPrice:     number;
}

export interface Transaction {
    waypointSymbol: string;
    shipSymbol:     string;
    tradeSymbol:    string;
    type:           string;
    units:          number;
    pricePerUnit:   number;
    totalPrice:     number;
    timestamp:      Date;
}
