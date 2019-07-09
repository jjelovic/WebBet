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
      id:    match.id,
      selectedInTO: selectedInTO
    }

    this.ticketHasTopOfferBet = this.ticketService.ticketFormData.ticketMatches.filter(el=> el.selectedInTO).length > 0;
    let existingPair = this.ticketService.ticketFormData.ticketMatches.filter(el=> el.id == this.ticketMatch.id)[0];
    

     if(typeof existingPair !== "undefined" ){
      
      //Same match selected but different qouta
      if(existingPair.quota !== this.ticketMatch.quota || this.ticketMatch.selectedInTO){
          

          let typeToSwitchPairIndex = this.ticketService.ticketFormData.ticketMatches.findIndex(el=> el.id == this.ticketMatch.id);
          this.ticketService.ticketFormData.ticketMatches[typeToSwitchPairIndex] =this.ticketMatch;
    
          //Update ticket
           this.ticketService.updateTotalCoefficient();
           this.ticketService.updatePossibleReturn();

           this.ticketService.validateTicketForm();

           this.ticketService.isSecondTopOfferMatchSelected = false;
           }
        }
        else {
     
           if(this.ticketMatch.quota !== null ) {

              //Cannot combine top offer matches, only one allowed
               if(!(this.ticketMatch.selectedInTO && this.ticketHasTopOfferBet)){

               this.ticketService.ticketFormData.ticketMatches.push(this.ticketMatch);
               this.ticketService.updateTotalCoefficient();
               this.ticketService.updateStakeWithMtCosts();
               this.ticketService.updatePossibleReturn();

               this.ticketService.validateTicketForm();
               this.ticketService.isSecondTopOfferMatchSelected = false;
            }
            else this.ticketService.isSecondTopOfferMatchSelected = true;

            this.ticketService.validateTicketForm();
          }
        }
  }
}
