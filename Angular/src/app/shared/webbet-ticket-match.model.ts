export class WebbetTicketMatch {

    pair   :string;
    id:number;
    type: string;
    quota: number;
    
    constructor(pair, id,type, quota){
        this.pair = pair;
        this.id = id;
        this.type = type;
        this.quota = quota;
    }
}
