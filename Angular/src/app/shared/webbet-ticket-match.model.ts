export class WebbetTicketMatch {

    pair   :string;
    matchId:number;
    type: string;
    quota: number;
    selectedInTopOffer: boolean;
    
    constructor(pair, matchId,type, quota, selectedInTopOffer){
        this.matchId = matchId;
        this.pair = pair;
        this.type = type;
        this.quota = quota;
        this.selectedInTopOffer = selectedInTopOffer;
    }
}
