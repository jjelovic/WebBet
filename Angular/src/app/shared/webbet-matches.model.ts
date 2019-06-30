import { element } from 'protractor';
import { WebbetOffer } from './webbet-offer.model';

export class WebbetMatches {
    public sport:   string;
    public matches: WebbetOffer[];
    

    constructor({sport, matches}){
        this.sport = sport;

        let matchArray = [];
        matches.array.forEach(function(element){
            matchArray.push(new WebbetOffer(
                                            element.id, 
                                            element.pair, 
                                            element.type1, 
                                            element.typeX, 
                                            element.type2, 
                                            element.type1X, 
                                            element.type2X, 
                                            element.type12, 
                                            element.isPartOfTopOffer, 
                                            element.sportId));
        });
        
        this.matches = matchArray;
    }
}