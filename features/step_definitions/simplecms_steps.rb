#visit('/search') fill_in('query', :with => query) click_button('Search')

When /^(?:|I )enter "([^"]*)" into "([^"]*)"$/ do |value, field|
    fill_in(field, :with => value)
end

