﻿@ignore
Feature: BenchmarkBandings
Benchmark API Bandings Endpoints

    Scenario: Sending a valid free school meal banding request
        Given a valid fsm banding request
        When I submit the banding request
        Then the free school meal banding result should be ok

    Scenario: Sending a valid school size banding request
        Given a valid school size banding request
        When I submit the banding request
        Then the school size banding result should be ok