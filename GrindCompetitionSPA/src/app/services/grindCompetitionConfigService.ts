import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GrindCompetitionConfig } from '../models/grindCompetitionConfig';

@Injectable({
  providedIn: 'root'
})
export class GrindCompetitionConfigService {
  private readonly baseUri: string = environment.backendApiAddress + '/api/GrindCompetitionConfig';

  constructor(
    private readonly http: HttpClient) { }

  public getConfig(): Observable<GrindCompetitionConfig> {
    return this.http.get<GrindCompetitionConfig>(this.baseUri);
  }
}
