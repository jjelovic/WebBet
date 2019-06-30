import { element } from 'protractor';

import { WebbetTicketMatch } from './webbet-ticket-match.model';
export class WebbetTicket {

    stake : number;
    stakeWithManipulatingCosts: number;
    possibleReturn: number;
    selectedMatches: WebbetTicketMatch[];
    totalMatchesCoeficient: number;

    constructor(stake, stakeWithManipulatingCosts, possibleReturn, selectedMatches, totalMatchesCoeficinet){
        
        let tempMatchArray = [];

        this.stake = stake;
        this.stakeWithManipulatingCosts = stakeWithManipulatingCosts;
        this.possibleReturn = possibleReturn;
        this.totalMatchesCoeficient = totalMatchesCoeficinet;
        
        selectedMatches.array.forEach(function(element){
            tempMatchArray.push( new WebbetTicketMatch(
                element.pair,
                element.id, 
                element.type,
                element.quota
            ));
        })
        
        this.selectedMatches = tempMatchArray;
    }
}
