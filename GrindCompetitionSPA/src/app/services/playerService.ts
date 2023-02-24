import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Player } from '../models/player';
import { map } from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private readonly baseUri: string = environment.backendApiAddress + '/api/GrindCompetitionPlayer';

  constructor(
    private readonly http: HttpClient) { }

  public getPlayers(): Observable<Player[]> {
    return this.http.get<Player[]>(this.baseUri).pipe(map(players => {
      for(const [index, player] of players.entries()) {
        player.place = index+1;
      }
      return players;
    }));
  }
}
