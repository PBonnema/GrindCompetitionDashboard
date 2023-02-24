import { Component } from '@angular/core';
import { GrindCompetitionConfig } from 'src/app/models/grindCompetitionConfig';
import { Player } from 'src/app/models/player';
import { GrindCompetitionConfigService } from 'src/app/services/grindCompetitionConfigService';
import { PlayerService } from 'src/app/services/playerService';
import { interval } from 'rxjs'

@Component({
  selector: 'app-grindingcompetition',
  templateUrl: './grindingcompetition.component.html',
  styleUrls: ['./grindingcompetition.component.scss']
})
export class GrindingCompetitionComponent {
  config: GrindCompetitionConfig | undefined = undefined;

  players: Player[] = [];
  displayedColumns = ['Name', 'XP Grinded', "Stats"];

  timeLeftOfEvent = '12 14:52:01';

  constructor(
      playerService: PlayerService,
      grindCompetitionConfigService: GrindCompetitionConfigService) {
    playerService.getPlayers().subscribe(value => {
      for (const player of value) {
        player.statsLink = `https://blocktanks.io/user/${player.name.toLowerCase()}`;
      }
      this.players = value;
    });
    grindCompetitionConfigService.getConfig().subscribe(value => this.config = value);

    interval(1000).subscribe(x => this.updateTimeLeftOfEvent(x));
  }

  private updateTimeLeftOfEvent(_: number)
  {
    const now = new Date().getTime();
    // Get the difference in time to get time left until reaches 0
    const t = new Date(this.config?.competitionEnd ?? new Date()).getTime() - now;

    // Check if time is above 0
    if (t > 0) {
      // Setup Days, hours, seconds and minutes
      // Algorithm to calculate days...
      const days = Math.floor(t / (1000 * 60 * 60 * 24));
      let daysStr = `${days}`;
      // prefix any number below 10 with a "0" E.g. 1 = 01
      if (days < 10) { daysStr = "0" + daysStr; }

      // Algorithm to calculate hours
      const hours = Math.floor((t % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      let hoursStr = `${hours}`;
      if (hours < 10) { hoursStr = "0" + hoursStr; }

      // Algorithm to calculate minutes
      const mins = Math.floor((t % (1000 * 60 * 60)) / (1000 * 60));
      let minsStr = `${mins}`;
      if (mins < 10) { minsStr = "0" + minsStr; }

      // Algorithm to calculate seconds
      const secs = Math.floor((t % (1000 * 60)) / 1000);
      let secsStr = `${secs}`;
      if (secs < 10) { secsStr = "0" + secsStr; }

      // Create Time String
      this.timeLeftOfEvent = `${daysStr} ${hoursStr} : ${minsStr} : ${secsStr}`;
    }
  }
}
