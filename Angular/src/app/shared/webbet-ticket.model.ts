import { element } from 'protractor';

import { WebbetTicketMatch } from './webbet-ticket-match.model';
export class WebbetTicket {

    Id: number;
    ticketCode: string;
    stake : number;
    stakeWithManipulationCosts: number;
    possibleReturn: number;
    ticketMatches: WebbetTicketMatch[];
    totalMatchesCoefficient: number;
    

    constructor(Id, stake,ticketCode, stakeWithManipulationCosts, possibleReturn, ticketMatches, totalMatchesCoefficient){
        
        let tempMatchArray = [];

        this.Id = Id;
        this.stake = stake;
        this.ticketCode = ticketCode,
        this.stakeWithManipulationCosts = stakeWithManipulationCosts;
        this.possibleReturn = possibleReturn;
        this.totalMatchesCoefficient = totalMatchesCoefficient;
        
        ticketMatches.array.forEach(function(element){
            tempMatchArray.push( new WebbetTicketMatch(
                element.pair,
                element.matchId, 
                element.type,
                element.quota,
                element.selectedInTopOffer
            ));
        })
        
        this.ticketMatches = tempMatchArray;
    }
}
