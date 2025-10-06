from education_benchmarking_and_insights import main


def test_find_all_taxis():
    taxis = main.find_all_taxis()
    assert taxis.count() > 5
