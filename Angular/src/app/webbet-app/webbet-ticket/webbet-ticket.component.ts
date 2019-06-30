import { WebbetAppService } from './../../shared/webbet-app.service';
import { Component, OnInit } from '@angular/core';
import { WebbetTicket } from 'src/app/shared/webbet-ticket.model';
import { WebbetTicketService } from 'src/app/shared/webbet-ticket.service';

@Component({
  selector: 'app-webbet-ticket',
  templateUrl: './webbet-ticket.component.html',
  styles: []
})
export class WebbetTicketComponent implements OnInit {

ticket: WebbetTicket;
  constructor(private service:WebbetAppService, private ticketService: WebbetTicketService) { }


  ngOnInit() {
   this.resetForm();
  }

  resetForm(){
    this.ticketService.ticketFormData = {
      stake: 0,
      stakeWithManipulatingCosts: 0,
      possibleReturn:0,
      totalMatchesCoeficient:0,
      selectedMatches: []
    }
  }
}
