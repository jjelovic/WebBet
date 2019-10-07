import { browser } from 'protractor';
import { WebbetTicketService } from './../../shared/webbet-ticket.service';
import { WebbetTicket } from './../../shared/webbet-ticket.model';
import { WebbetTicketMatch } from '../../shared/webbet-ticket-match.model';
import { WebbetOffer } from './../../shared/webbet-offer.model';
import { Component, OnInit } from '@angular/core';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
import { WebbetMatches } from 'src/app/shared/webbet-matches.model';
import { element } from '@angular/core/src/render3/instructions';
import { Ee } from '@angular/core/src/render3';
import { pairs } from 'rxjs';
import { BetTypeEnum } from 'src/app/enums/betTypes';

@Component({
  selector: 'app-webbet-offer',
  templateUrl: './webbet-offer.component.html',
  styles: []
})
export class WebbetOfferComponent implements OnInit {

  offerList: WebbetMatches[];
  topOfferList: WebbetMatches[];
  
  ticketMatches: WebbetTicketMatch[];
  ticket: WebbetTicket;
  ticketMatch:WebbetTicketMatch;
  matchArray = [];
  topOfferMatchesLenght: number;
  topOfferMatch : WebbetTicketMatch;
  betTypeEnum = BetTypeEnum;
 
  ticketHasTopOfferBet: boolean;
  isSecondTopOfferMatchSelected: boolean;
  tempHasSpecialOfferPair = false; 
  
  constructor(private service: WebbetAppService,
              private ticketService: WebbetTicketService) { }

  ngOnInit() { 
    this.service.getWebbetMatches().subscribe(res => {
        this.offerList = res as WebbetMatches[];

        this.topOfferList = this.offerList.filter((element) =>
                          element.matches
                          .some((subElement) => subElement.isPartOfTopOffer))
                          .map(element => {
                          return Object.assign( {topOfferMatches: element.matches.filter(subElement => subElement.isPartOfTopOffer )})
        });
      });
  }

  addMatchToTicket(match: WebbetOffer, quota:number, type : string, selectedInTO: boolean ){
    
    this.ticketMatch = {
      pair:  match.pair,
      quota: selectedInTO? quota * 1.05 : quota,
      type:  type,
      matchId:    match.id,
      selectedInTopOffer: selectedInTO
    }

    

    this.ticketHasTopOfferBet = this.ticketService.ticketFormData.ticketMatches.filter(el=> el.selectedInTopOffer).length > 0;
    let existingPair = this.ticketService.ticketFormData.ticketMatches.filter(el=> el.matchId == this.ticketMatch.matchId)[0];
    
    if(typeof existingPair !== "undefined" ){

      let topOfferMatchAllowedToEdit = this.topOfferMatch != null ? (existingPair.matchId === this.topOfferMatch.matchId) && existingPair.selectedInTopOffer: false;
      if(this.topOfferMatch!= null) console.log(topOfferMatchAllowedToEdit)
 
      //Same match selected but different qouta
      if(existingPair.quota !== this.ticketMatch.quota ){

        let typeToSwitchPairIndex = this.ticketService.ticketFormData.ticketMatches.findIndex(el=> el.matchId == this.ticketMatch.matchId);
        this.ticketService.ticketFormData.ticketMatches[typeToSwitchPairIndex] = this.ticketMatch;
  
        //Update ticket
          this.ticketService.updateTotalCoefficient();
          this.ticketService.updatePossibleReturn();

          this.ticketService.validateTicketForm();

          this.ticketService.isSecondTopOfferMatchSelected = false;

          if (selectedInTO) {
            match.selectedTopOfferType = type;
            match.selectedType = null;
         
          } else {
            match.selectedType = type;
            match.selectedTopOfferType = null
          }
      }
    } else {
     
      if(this.ticketMatch.quota !== null ) {

        //Cannot combine top offer matches, only one allowed
        if(!(this.ticketMatch.selectedInTopOffer && this.ticketHasTopOfferBet)){
  
          this.ticketService.ticketFormData.ticketMatches.push(this.ticketMatch);
          this.ticketService.matchArray.push(match);
          
          this.topOfferMatch = this.ticketMatch.selectedInTopOffer? this.ticketMatch : null;
          

          this.ticketService.updateTotalCoefficient();
          this.ticketService.updateStakeWithMtCosts();
          this.ticketService.updatePossibleReturn();

          this.ticketService.validateTicketForm();
          this.ticketService.isSecondTopOfferMatchSelected = false;

          if (selectedInTO) {
            match.selectedTopOfferType = type;
            match.selectedType = null;
          } else {
            match.selectedType = type;
            match.selectedTopOfferType = null
          }
        }
        else this.ticketService.isSecondTopOfferMatchSelected = true;

        this.ticketService.validateTicketForm();
      }
    }
  }
}
