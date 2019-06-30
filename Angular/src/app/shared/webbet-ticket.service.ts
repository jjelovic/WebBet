import { WebbetTicket } from './webbet-ticket.model';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WebbetTicketService {

  ticketFormData: WebbetTicket;

  constructor() { }
}
