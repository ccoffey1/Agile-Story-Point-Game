import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { faCopy } from '@fortawesome/free-solid-svg-icons';
import { NgbPopover } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-game-stage',
  templateUrl: './game-stage.component.html',
  styleUrls: ['./game-stage.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class GameStageComponent implements OnInit {

  // general properties
  teamName: string;
  joinCode: string;

  // popout properties
  faCopy = faCopy
  popoverCount = 1
  popoverReset: NodeJS.Timeout;
  popoverMessages = [
    "Copied!",
    "Double copy!",
    "Triple Copy!",
    "Copying spree!",
    "Rampage!",
    "Dominating!",
    "Unstoppable!",
    "Wicked sick!"
  ]
  popoverMessage = this.popoverMessages[0];

  // reasons panel properties
  reasons = [
    { title: '¯\\_(ツ)_/¯', description: "I have no idea.", selected: false },
    { title: 'New Territory', description: "We're unfamiliar with this area.", selected: false },
    { title: 'Minimal Dev Work', description: "Won't take too long.", selected: false },
    { title: 'Medium Dev Work', description: "Quite a bit of work.", selected: false },
    { title: 'Tons of Dev Work', description: "Hopefully we'll finish by retirement.", selected: false },
    { title: 'QA Buffer', description: "More time for QE work.", selected: false }
  ]

  // point-selection card properties
  pointOptions = [ 3, 5, 8, 13, 21, 32 ]

  constructor() { }

  ngOnInit(): void {
    // TODO: Fetch dynamically
    this.teamName = this.truncate('Funderdome', 30)
    this.joinCode = '1d7279b1-80a4-4389-a775-3c142d2f12b3'
  }

  truncate(str, n){
    return (str.length > n) ? str.substr(0, n-1) + '…' : str;
  };

  popoutTriggered(popover: NgbPopover) {
    if (popover.isOpen() === false) {

      popover.open()

      // ready the popover for the next click
      // apparently it only updates on open or something
      if (this.popoverCount < this.popoverMessages.length) {
        popover.popoverClass = ''
        this.popoverMessage = this.popoverMessages[this.popoverCount]
      } else {
        popover.popoverClass = 'godlike-shake'
        this.popoverMessage = 'GODLIKE!!!!!!'
      }

      // reset a few clicks after GODLIKE
      this.popoverCount = (this.popoverCount + 1) % (this.popoverMessages.length + 5)

      setTimeout(() => popover.close(), 800)

      // reset popover if the user doesn't click for another second
      if (this.popoverReset) {
        clearTimeout(this.popoverReset)
      }
      this.popoverReset = setTimeout(() => {
        this.popoverCount = 1;
        this.popoverMessage = this.popoverMessages[0]
        popover.popoverClass = ''
      }, 1800)
    }
  }

  /**
   * (For debugging) Print out reasons array.
   */
  printReasons() {
    console.table(this.reasons)
  }
}
