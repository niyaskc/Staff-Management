import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-confirm-dialoge',
  templateUrl: './confirm-dialoge.component.html',
  styleUrls: ['./confirm-dialoge.component.css']
})
export class ConfirmDialogeComponent implements OnInit {

  @Input() isVisible: boolean;
  @Input() title: string;
  @Input() message: string;
  @Input() yesFn: () => void;
  @Input() noFn: () => void;

  constructor() { }

  ngOnInit(): void {
  }

}
