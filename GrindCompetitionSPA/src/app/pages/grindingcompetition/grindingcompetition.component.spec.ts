import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrindingCompetitionComponent } from './grindingcompetition.component';

describe('GrindingCompetitionComponent', () => {
  let component: GrindingCompetitionComponent;
  let fixture: ComponentFixture<GrindingCompetitionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrindingCompetitionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrindingCompetitionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
