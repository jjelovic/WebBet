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

  constructor(private service: WebbetAppService,
              private ticketService: WebbetTicketService) { }

  ngOnInit() { 
    this.service.getWebbetMatches().subscribe(res => {
        this.offerList = res as WebbetMatches[];

        this.topOfferList = this.offerList.filter((element) =>
                          element.matches
                          .some((subElement) => subElement.isPartOfTopOffer))
                          .map(element => {
                          return Object.assign( {topOfferMatches: element.matches.filter(subElement => subElement.isPartOfTopOffer ) })
        });

        // console.log(this.topOfferList);
      });
  }

  addMatchToTicket(match: WebbetOffer, quota:number, type:string ){
     console.log("kvota i tip: " +  quota, type)

    this.ticketMatch = {
      pair:  match.pair,
      quota: quota,
      type:  type,
      id:    match.id
    }

    let tempPair = this.ticketService.ticketFormData.selectedMatches.filter(el=> el.id == this.ticketMatch.id)[0];


    if(!tempPair) {
       
      if(this.ticketMatch.quota !== null ) {
        this.ticketService.ticketFormData.selectedMatches.push(this.ticketMatch);
        this.ticketService.ticketFormData.totalMatchesCoeficient = this.getTotalCoeficient();
        this.ticketService.ticketFormData.possibleReturn = this.getPossibleReturn();
        this.ticketService.ticketFormData.stakeWithManipulatingCosts = this.getStakeWithManCosts();
      }
    }
    console.log("Man troškovi:" + this.ticketService.ticketFormData.stakeWithManipulatingCosts);
    console.log(this.ticketService.ticketFormData);
    console.log(this.ticketService.ticketFormData.totalMatchesCoeficient)
  }



  getTotalCoeficient(){
     let totalCoeficient =  this.ticketService.ticketFormData.selectedMatches
     .reduce(function(acc,qu) { return acc * qu.quota; }, 1); 

     return parseFloat(totalCoeficient.toFixed(2));
  }

  getPossibleReturn(){
    let possibleReturn = this.ticketService.ticketFormData.stakeWithManipulatingCosts * this.ticketService.ticketFormData.totalMatchesCoeficient;

    return parseFloat(possibleReturn.toFixed(2));
  }

  getStakeWithManCosts(){
    return (this.ticketService.ticketFormData.stake - (this.ticketService.ticketFormData.stake * 0.05))
  }

  onDelete(index: number){
    this.ticketService.ticketFormData.selectedMatches.splice(index,1);
  }

}
