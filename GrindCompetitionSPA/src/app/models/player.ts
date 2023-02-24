import { Score } from "./score";

export interface Player {
    place: number;
    name: string;
    initialScore: Score;
    currentScore: Score;
    statsLink: string;
}