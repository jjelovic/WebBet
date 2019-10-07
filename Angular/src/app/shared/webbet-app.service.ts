import { WebbetTicket } from 'src/app/shared/webbet-ticket.model';

import { WebbetMatches } from './webbet-matches.model';
import { WebbetOffer } from './webbet-offer.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { WebbetWallet } from './webbet-wallet.model';
import { NgForm } from '@angular/forms';


@Injectable({
  providedIn: 'root'
})
export class WebbetAppService {

  walletFormData : WebbetWallet;

  readonly rootURL = 'http://localhost:51157/api';


  constructor(private http: HttpClient) { }

  getWebbetMatches(): Observable<any> {
    return this.http.get(this.rootURL + '/Matches');
  }

  postWalletDeposit(walletFormData:WebbetWallet){
    return this.http.post(this.rootURL + '/Wallet', walletFormData);
  }

  getWalletBalance():Observable<any> {
    return this.http.get(this.rootURL + '/Wallet');
  }

  getAllTickets():Observable<any>{
    return this.http.get(this.rootURL + '/Ticket');
  }

  postWebbetTicket(ticketFormData: WebbetTicket){
    return this.http.post(this.rootURL + '/Ticket', ticketFormData);
  }

  deleteWebbetTicket(webbetTicketCode: string){
    return this.http.delete(this.rootURL + '/Ticket/' + webbetTicketCode);
  }

  register( registrationBody: any){
    return this.http.post(this.rootURL + '/ApplicationUser/Register', registrationBody );
  }

  login(formData: NgForm){
    return this.http.post(this.rootURL + '/ApplicationUser/Login', formData );
  }

  getUserDetalis():Observable<any>{
    return this.http.get(this.rootURL + '/UserProfile' );
  }

}