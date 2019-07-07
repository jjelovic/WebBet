import { element } from 'protractor';

import { WebbetTicketMatch } from './webbet-ticket-match.model';
export class WebbetTicket {

    ticketCode: string;
    stake : number;
    stakeWithManipulationCosts: number;
    possibleReturn: number;
    ticketMatches: WebbetTicketMatch[];
    totalMatchesCoefficient: number;
    

    constructor(stake,ticketCode, stakeWithManipulationCosts, possibleReturn, ticketMatches, totalMatchesCoefficient){
        
        let tempMatchArray = [];

        this.stake = stake;
        this.ticketCode = ticketCode,
        this.stakeWithManipulationCosts = stakeWithManipulationCosts;
        this.possibleReturn = possibleReturn;
        this.totalMatchesCoefficient = totalMatchesCoefficient;
        
        ticketMatches.array.forEach(function(element){
            tempMatchArray.push( new WebbetTicketMatch(
                element.pair,
                element.id, 
                element.type,
                element.quota,
                element.selectedInTO
            ));
        })
        
        this.ticketMatches = tempMatchArray;
    }
}
