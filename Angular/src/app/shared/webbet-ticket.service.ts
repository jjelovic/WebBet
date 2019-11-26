import { WebbetTicketMatch } from './webbet-ticket-match.model';
import { WebbetAppService } from './webbet-app.service';
import { WebbetWallet } from 'src/app/shared/webbet-wallet.model';
import { WebbetTicket } from './webbet-ticket.model';
import { Injectable } from '@angular/core';
import { WebbetOffer } from './webbet-offer.model';
import { WebbetUserService } from './webbet-user.service';


@Injectable({
  providedIn: 'root'
})
export class WebbetTicketService {

  constructor(private service : WebbetAppService, private userService : WebbetUserService) { }

  ticketFormData: WebbetTicket;
  wallet : WebbetWallet = new WebbetWallet(0);


  isSecondTopOfferMatchSelected: boolean;
  topOfferPairPlus5: boolean;
  invalidStake: boolean;
  stakeGreatherThenWalletAmount: boolean;

  ticketMatches : WebbetTicketMatch[];
  ticketMatchesForPreview: WebbetTicketMatch[];
  acitveTicketCode: string;
  matchArray: WebbetOffer[] = [] ;

  updateTotalCoefficient(){
    let totalCoefficient =  this.ticketFormData.ticketMatches.reduce(function(acc,qu) { return acc * qu.quota; }, 1); 

     this.ticketFormData.totalMatchesCoefficient= parseFloat(totalCoefficient.toFixed(2));
 }

 updatePossibleReturn(){
   let possibleReturn = this.ticketFormData.stakeWithManipulationCosts * this.ticketFormData.totalMatchesCoefficient;

   this.ticketFormData.possibleReturn = this.ticketFormData.ticketMatches.length > 0 ? parseFloat(possibleReturn.toFixed(2)) : 0 ;
 }

 updateStakeWithMtCosts(){
   this.ticketFormData.stakeWithManipulationCosts = (this.ticketFormData.stake - (this.ticketFormData.stake * 0.05))
 }

 updateWallateBalance() {
  let user = this.userService.userDetails;

  this.service.getWalletBalance(user.userId).subscribe( res => {
    this.wallet = res as WebbetWallet;
  });
 }


validateTicketForm(){

  this.topOfferPairPlus5 =  (this.ticketFormData.ticketMatches.filter(el=> el.selectedInTopOffer).length !== 0
                                && this.ticketFormData.ticketMatches.length < 6); 

  this.stakeGreatherThenWalletAmount = this.ticketFormData.stake > this.wallet.amount;
}

}
