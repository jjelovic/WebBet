export class WebbetTicketMatch {

    pair   :string;
    id:number;
    type: string;
    quota: number;
    selectedInTO: boolean;
    
    constructor(pair, id,type, quota, selectedInTO){
        this.id = id;
        this.pair = pair;
        this.type = type;
        this.quota = quota;
        this.selectedInTO = selectedInTO;
    }
}
